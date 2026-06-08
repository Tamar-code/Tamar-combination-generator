import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PermutationService } from '../../service/permutation-service';

@Component({
  selector: 'app-navigator',
  standalone: true,
  imports: [],
  templateUrl: './navigator.html',
  styleUrl: './navigator.css',
})
export class NavigatorComponent implements OnInit {
  readonly permService = inject(PermutationService);
  private readonly router = inject(Router);

  ngOnInit(): void {
    // If user navigated back from results and there's a current index > 0,
    // restore the current permutation state to prevent empty display
    const currentIndexNum = parseInt(this.permService.currentIndex(), 10);
    if (currentIndexNum > 0 && this.permService.currentPermutation().length === 0) {
      // Reload the current permutation by computing it at the current index
      this.permService.loadCurrentPermutation();
    }
  }

  onNext(): void {
    this.permService.getNext();
  }

  onReset(): void {
    this.permService.reset();
    this.router.navigate(['']);
  }

  onShowAll(): void {
    this.router.navigate(['/results']);
  }
}
