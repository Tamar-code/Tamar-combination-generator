import { Component, inject, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { PaginationComponent } from '../pagination/pagination';
import { GetAllResponse } from '../../models/permutationModel';
import { PermutationService } from '../../service/permutation-service';

@Component({
  selector: 'app-all-permutations',
  standalone: true,
  imports: [PaginationComponent],
  templateUrl: './all-permutations.html',
  styleUrl: './all-permutations.css',
})
export class AllPermutationsComponent implements OnInit {
  readonly permService = inject(PermutationService);
  private readonly router = inject(Router);

  permutations = signal<{ index: number; values: number[] }[]>([]);
  totalPages = signal<number>(0);
  currentPage = signal<number>(1);
  readonly pageSize = 10;
  private readonly startFromIndex = signal<number>(0);

  ngOnInit(): void {
    this.startFromIndex.set(this.permService.currentIndex());
    this.permService.allPermutationsCurrentIndex.set(this.startFromIndex());
    this.loadPage(1);
  }

  loadPage(page: number): void {
    this.currentPage.set(page);
    const fromIndex = this.startFromIndex() + (page - 1) * this.pageSize;
    this.permService.allPermutationsCurrentIndex.set(fromIndex);
    
    this.permService.getAll(page, this.pageSize, (res: GetAllResponse) => {
      this.permutations.set(
        res.permutations.map(p => ({ index: p.index, values: p.permutation }))
      );
      this.totalPages.set(res.totalPages);
    });
  }

  onPageChange(page: number): void {
    this.loadPage(page);
  }

  onBack(): void {
    const lastShown = this.permutations()[this.permutations().length - 1];
    if (lastShown) {
      this.permService.currentIndex.set(lastShown.index);
      this.permService.currentPermutation.set(lastShown.values);
    }
    this.router.navigate(['/navigate']);
  }
}
