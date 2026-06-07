import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { PermutationService } from '../../service/permutation-service';

@Component({
  selector: 'app-input',
  standalone: true,
  imports: [],
  templateUrl: './input.html',
  styleUrl: './input.css',
})
export class InputComponent {
  readonly permService = inject(PermutationService);
  private readonly router = inject(Router);

  isValid(): boolean {
    const n = this.permService.inputNumber();
    return Number.isInteger(n) && n >= 1 && n <= 20;
  }

  isInteger(n: number): boolean {
    return Number.isInteger(n);
  }

  onStart(): void {
    if (this.isValid()) {
      this.permService.start(() => {
        this.router.navigate(['/navigate']);
      });
    }
  }
}
