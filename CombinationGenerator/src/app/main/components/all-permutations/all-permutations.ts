import { Component, inject, signal, afterNextRender } from '@angular/core';
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
export class AllPermutationsComponent {
  readonly permService = inject(PermutationService);
  private readonly router = inject(Router);

  permutations = signal<{ index: string; values: number[] }[]>([]);
  totalPages = signal<number>(0);
  currentPage = signal<number>(1);
  readonly pageSize = 10;
  private readonly startFromIndex = signal<string>('0');

  _ = afterNextRender(() => {
    this.startFromIndex.set(this.permService.currentIndex());
    this.permService.allPermutationsCurrentIndex.set(this.startFromIndex());
    this.loadPage(1);
  });

  loadPage(page: number): void {
    this.currentPage.set(page);
    const startIndex = BigInt(this.startFromIndex());
    const fromIndex = startIndex + BigInt(page - 1) * BigInt(this.pageSize);
    this.permService.allPermutationsCurrentIndex.set(fromIndex.toString());

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
    // Preserve the last visible permutation from before entering "all permutations".
    // The service already holds the current index and permutation state.
    this.router.navigate(['/navigate']);
  }
}