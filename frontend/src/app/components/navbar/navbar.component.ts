import { Component, computed, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { MessageService } from 'primeng/api';

import { AuthService } from '../../modules/login/services/auth.service';

@Component({
    selector: 'app-navbar',
    imports: [RouterLink, ButtonModule, DialogModule],
    templateUrl: './navbar.component.html',
    styleUrl: './navbar.component.css',
})
export class NavbarComponent {
    private readonly authService = inject(AuthService);
    private readonly router = inject(Router);
    private readonly messageService = inject(MessageService);

    isInfoDialogVisible = false;
    isLogOutDialogVisible = false;

    currentUserName = computed(() => this.authService.currentUser()?.fullName ?? 'Login');

    hasLoggedInUser = computed(() => !!this.authService.currentUser());

    showDialog() {
        this.isInfoDialogVisible = true;
    }

    showLogOutDialog() {
        this.isLogOutDialogVisible = true;
    }

    signOut() {
        try {
            this.authService.signOut();
            this.showToastSuccess();
        } catch {
            this.showToastError();
        } finally {
            this.router.navigate(['/login']);
            this.isLogOutDialogVisible = false;
        }
    }

    showToastSuccess() {
        this.messageService.add({
            key: 'globalToast',
            severity: 'success',
            summary: 'You have successfully logged out',
            life: 3000,
        });
    }

    showToastError() {
        this.messageService.add({
            key: 'globalToast',
            severity: 'error',
            summary: 'Something went wrong',
            detail: 'Please try again later',
            life: 3000,
        });
    }
}
