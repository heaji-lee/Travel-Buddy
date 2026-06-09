import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { AuthService } from '../../services/auth.service';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        ButtonModule,
        DividerModule,
        InputTextModule,
        DialogModule,
    ],
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})
export class LoginComponent {
    private readonly fb = inject(FormBuilder);
    private readonly authService = inject(AuthService);
    private readonly router = inject(Router);

    loginForm = this.fb.nonNullable.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required],
    });

    signUpForm = this.fb.nonNullable.group({
        fullName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required],
    });

    isSignUpDialogVisible = false;
    loginError = '';
    signUpError = '';

    showSignUpDialog() {
        this.isSignUpDialogVisible = true;
    }

    login() {
        if (this.loginForm.invalid) {
            this.loginError = 'Please enter a valid email and password.';
            return;
        }

        const credentials = this.loginForm.getRawValue();

        this.authService.login(credentials).subscribe({
            next: (response) => {
                console.log('Logged in', response);
                this.loginError = '';
                this.router.navigate(['/home']);
            },
            error: (err) => {
                this.loginError =
                    err?.error?.message || 'Login failed. Please verify your credentials.';
            },
        });
    }

    signUp() {
        if (this.signUpForm.invalid) {
            this.signUpError = 'Please fill in all sign-up fields.';
            return;
        }

        const details = this.signUpForm.getRawValue();

        this.authService.signUp(details).subscribe({
            next: (response) => {
                console.log('Signed up', response);
                this.signUpError = '';
                this.isSignUpDialogVisible = false;
                this.loginForm.patchValue({ email: response.email });
            },
            error: (err) => {
                this.signUpError = err?.error?.message || 'Registration failed. Please try again.';
            },
        });
    }
}
