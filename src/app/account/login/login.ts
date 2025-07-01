import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../../core/openapi';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router'; // Add this import

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  private accountService = inject(AccountService);
  private router = inject(Router);
  form: FormGroup;
  error: string | null = null;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  submit() {
    if (this.form.valid) {
      this.accountService.apiAccountPost(this.form.value).subscribe({
        next: (res) => {
          localStorage.setItem('token', res);
          this.error = null;
          this.router.navigate(['/admin']);
        },
        error: (err) => {
          this.error = 'Login failed. Please check your credentials.';
        }
      });
    }
  }
}
