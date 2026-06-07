import { Component, input, output, signal, effect } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pagination',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './pagination.html',
  styleUrl: './pagination.css',
})
export class PaginationComponent {
  currentPage = input<number>(1);
  totalPages = input<number>(1);
  pageChange = output<number>();
  jumpToPage = signal<number>(1);

  private readonly syncEffect = effect(() => {
    this.jumpToPage.set(this.currentPage());
  });

  isValidPage(): boolean {
    const page = this.jumpToPage();
    return Number.isInteger(page) && page >= 1 && page <= this.totalPages();
  }

  goFirst(): void {
    this.emit(1);
  }

  goPrev(): void {
    if (this.currentPage() > 1) {
      this.emit(this.currentPage() - 1);
    }
  }

  goNext(): void {
    if (this.currentPage() < this.totalPages()) {
      this.emit(this.currentPage() + 1);
    }
  }

  goLast(): void {
    this.emit(this.totalPages());
  }

  goToPage(): void {
    if (this.isValidPage()) {
      this.emit(this.jumpToPage());
    }
  }

  private emit(page: number): void {
    this.jumpToPage.set(page);
    this.pageChange.emit(page);
  }
}
