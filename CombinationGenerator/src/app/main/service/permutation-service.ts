import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { computed, inject, Injectable, signal } from '@angular/core';
import { GetAllResponse, NextResponse, StartResponse } from '../models/permutationModel';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class PermutationService {
  private readonly http = inject(HttpClient);
  private readonly url = environment.apiUrl;

  public inputNumber = signal<number>(1);
  public totalCount = signal<string>('0');
  public currentIndex = signal<number>(0);
  public currentPermutation = signal<number[]>([]);
  public allPermutationsCurrentIndex = signal<number>(0);
  public hasMore = signal<boolean>(true);
  public loading = signal<boolean>(false);
  public errorMsg = signal<string>('');

  public totalCountDisplay = computed(() => 
    BigInt(this.totalCount()).toLocaleString('he-IL')
  );

  private handleHttpError(err: HttpErrorResponse, context: string): string {
    if (err.status === 0) {
      return 'שגיאה בחיבור לשרת - בדוק שהשרת פועל';
    } else if (err.status === 400) {
      return `${context} - בקשה לא תקינה`;
    } else if (err.status === 404) {
      return `${context} - המשאב לא נמצא`;
    } else if (err.status === 500) {
      return `${context} - שגיאת שרת פנימית`;
    } else if (err.status === 408 || (err as any).name === 'TimeoutError') {
      return `${context} - הבקשה ארכה יותר מדי זמן`;
    }
    return `${context} (שגיאה ${err.status})`;
  }

  start(onSuccess?: () => void): void {
    this.loading.set(true);
    this.errorMsg.set('');
    this.http.post<StartResponse>(`${this.url}/start`, { n: this.inputNumber() }).subscribe({
      next: (res: StartResponse) => {
        this.totalCount.set(res.totalCount);
        this.currentIndex.set(0);
        this.currentPermutation.set([]);
        this.hasMore.set(true);
        this.loading.set(false);
        onSuccess?.();
      },
      error: (err: HttpErrorResponse) => {
        this.errorMsg.set(this.handleHttpError(err, 'שגיאה בהתחלת חישוב'));
        this.loading.set(false);
      },
    });
  }

  getNext(): void {
    this.loading.set(true);
    this.errorMsg.set('');
    this.http.get<NextResponse>(`${this.url}/next`).subscribe({
      next: (res: NextResponse) => {
        this.currentIndex.set(res.index);
        this.currentPermutation.set(res.permutation);
        this.hasMore.set(res.hasMore);
        this.loading.set(false);
      },
      error: (err: HttpErrorResponse) => {
        this.errorMsg.set(this.handleHttpError(err, 'שגיאה בטעינת הקומבינציה'));
        this.loading.set(false);
      },
    });
  }

  getAll(pageNumber: number, pageSize = 10, onSuccess?: (res: GetAllResponse) => void): void {
    this.loading.set(true);
    this.errorMsg.set('');
    this.http.get<GetAllResponse>(`${this.url}/all`, {
      params: { page: pageNumber, pageSize: pageSize, fromIndex: this.allPermutationsCurrentIndex() },
    }).subscribe({
      next: (res: GetAllResponse) => {
        this.loading.set(false);
        onSuccess?.(res);
      },
      error: (err: HttpErrorResponse) => {
        this.errorMsg.set(this.handleHttpError(err, 'שגיאה בטעינת הרשימה'));
        this.loading.set(false);
      },
    });
  }

  reset(): void {
    this.totalCount.set('0');
    this.currentIndex.set(0);
    this.currentPermutation.set([]);
    this.hasMore.set(true);
    this.errorMsg.set('');
    this.inputNumber.set(1);
  }
}
