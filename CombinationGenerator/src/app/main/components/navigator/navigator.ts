import { Component, inject, afterNextRender } from '@angular/core';
import { Router } from '@angular/router';
import { PermutationService } from '../../service/permutation-service';

@Component({
  selector: 'app-navigator',
  standalone: true,
  imports: [],
  templateUrl: './navigator.html',
  styleUrl: './navigator.css',
})
export class NavigatorComponent {
  readonly permService = inject(PermutationService);
  private readonly router = inject(Router);

  _ = afterNextRender(() => {
    // If user navigated back from results and there's a current index > 0,
    // restore the current permutation state to prevent empty display
    const currentIndexNum = parseInt(this.permService.currentIndex(), 10);
    if (currentIndexNum > 0 && this.permService.currentPermutation().length === 0) {
      this.permService.loadCurrentPermutation();
    }
  });

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