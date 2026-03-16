import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { MessageModule } from 'primeng/message';

import { AuthService } from '../../services/auth.service';

@Component({
    selector: 'app-login.component',
    imports: [
        ButtonModule,
        DividerModule,
        InputTextModule,
        DialogModule,
        ReactiveFormsModule,
        MessageModule,
    ],
    templateUrl: './login.component.html',
    styleUrl: './login.component.css',
})
export class LoginComponent {
    private readonly fb = inject(FormBuilder);
    private readonly authService = inject(AuthService);
    private readonly router = inject(Router);

    isLoginDialogVisible = false;
    loginError = signal<string | null>(null);
    signUpError = signal<string | null>(null);
    isLoading = signal(false);

    loginForm = this.fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required],
    });

    signUpForm = this.fb.group({
        fullName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(8)]],
    });

    showLoginDialog() {
        this.isLoginDialogVisible = true;
        this.signUpError.set(null);
        this.signUpForm.reset();
    }

    login() {
        if (this.loginForm.invalid) {
            this.loginForm.markAllAsTouched();
            return;
        }

        this.isLoading.set(true);
        this.loginError.set(null);

        const { email, password } = this.loginForm.value;

        this.authService.login({ email: email!, password: password! }).subscribe({
            next: () => {
                this.isLoading.set(false);
                this.router.navigate(['/']);
            },
            error: (err) => {
                this.isLoading.set(false);
                this.loginError.set(err?.error?.message ?? 'Invalid email or password.');
            },
        });
    }

    signUp() {
        if (this.signUpForm.invalid) {
            this.signUpForm.markAllAsTouched();
            return;
        }

        this.isLoading.set(true);
        this.signUpError.set(null);

        const { fullName, email, password } = this.signUpForm.value;

        this.authService
            .signUp({ fullName: fullName!, email: email!, password: password! })
            .subscribe({
                next: () => {
                    this.isLoading.set(false);
                    this.isLoginDialogVisible = false;
                    this.signUpForm.reset();
                },
                error: (err) => {
                    this.isLoading.set(false);
                    this.loginError.set(err?.error?.message ?? 'Sign up failed. Please try again.');
                },
            });
    }
}
