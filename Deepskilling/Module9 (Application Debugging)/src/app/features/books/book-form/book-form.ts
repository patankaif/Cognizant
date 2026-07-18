import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BookService } from '../../../core/services/book.service';

export function futureYearValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const currentYear = new Date().getFullYear();
    const value = Number(control.value);
    return value > currentYear ? { futureYear: { max: currentYear, actual: value } } : null;
  };
}

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './book-form.html',
  styleUrl: './book-form.css'
})
export class BookForm {
  private readonly fb = inject(FormBuilder);
  private readonly bookService = inject(BookService);
  private readonly router = inject(Router);

  readonly form = this.fb.nonNullable.group({
    title: ['', [Validators.required, Validators.maxLength(300)]],
    author: ['', [Validators.required, Validators.maxLength(150)]],
    genre: ['Fiction', Validators.required],
    publishedYear: [2024, [Validators.required, Validators.min(1450), futureYearValidator()]],
    price: [0, [Validators.required, Validators.min(0)]]
  });

  submitting = false;
  submitError: string | null = null;

  get f() {
    return this.form.controls;
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting = true;
    this.submitError = null;

    this.bookService.createBook(this.form.getRawValue()).subscribe({
      next: created => this.router.navigate(['/books', created.id]),
      error: () => {
        this.submitting = false;
        this.submitError = 'Failed to create book. Please try again.';
      }
    });
  }
}
