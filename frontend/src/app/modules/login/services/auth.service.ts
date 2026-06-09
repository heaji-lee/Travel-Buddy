import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { API_URL } from '../../../shared/constants';
import { LoginRequest, SignUpRequest, AuthResponse, User } from '../models/auth.model';
import { signal } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private readonly http = inject(HttpClient);

    currentUser = signal<User | null>(this.getStoredUser());

    login(credentials: LoginRequest) {
        return this.http.post<AuthResponse>(`${API_URL}/api/auth/login`, credentials);
    }

    signUp(details: SignUpRequest) {
        return this.http.post<AuthResponse>(`${API_URL}/api/auth/register`, details);
    }

    setCurrentUser(user: User | null) {
        this.currentUser.set(user);

        if (user) {
            localStorage.setItem('currentUserFullName', user.fullName);
            localStorage.setItem('currentUserEmail', user.email);
        } else {
            localStorage.removeItem('currentUserFullName');
            localStorage.removeItem('currentUserEmail');
        }
    }

    signOut() {
        this.setCurrentUser(null);
        this.setAccessToken(null);
    }

    setAccessToken(token: string | null) {
        if (token) {
            localStorage.setItem('accessToken', token);
        } else {
            localStorage.removeItem('accessToken');
        }
    }

    private getStoredUser(): User | null {
        const fullName = localStorage.getItem('currentUserFullName');
        const email = localStorage.getItem('currentUserEmail');

        if (!fullName || !email) {
            return null;
        }

        return { fullName, email };
    }
}
