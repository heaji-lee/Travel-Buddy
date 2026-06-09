import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
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

    isInfoDialogVisible = false;

    get currentUserName(): string {
        return this.authService.currentUser()?.fullName ?? 'Login';
    }

    get hasLoggedInUser(): boolean {
        return !!this.authService.currentUser();
    }

    showDialog() {
        this.isInfoDialogVisible = true;
    }

    signOut() {
        this.authService.signOut();
        this.router.navigate(['/login']);
    }
}
