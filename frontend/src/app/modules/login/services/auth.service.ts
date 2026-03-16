import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { API_URL } from '../../../shared/constants';
import { LoginRequest, SignUpRequest, AuthResponse } from '../models/auth.model';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private readonly http = inject(HttpClient);

    login(credentials: LoginRequest) {
        return this.http.post<AuthResponse>(`${API_URL}/api/login`, credentials);
    }

    signUp(details: SignUpRequest) {
        return this.http.post<AuthResponse>(`${API_URL}/api/login/register`, details);
    }
}
