import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { PermutationService } from '../service/permutation-service';

/** מונע גישה למסכים לפני שהתחילו חישוב */
export const permutationGuard: CanActivateFn = () => {
  const permService = inject(PermutationService);
  const router = inject(Router);

  if (permService.totalCount() !== '0') {
    return true;
  }

  return router.createUrlTree(['']);
};
