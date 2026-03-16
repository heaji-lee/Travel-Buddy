export interface LoginRequest {
    email: string;
    password: string;
}

export interface SignUpRequest {
    fullName: string;
    email: string;
    password: string;
}

export interface AuthResponse {
    id: string;
    email: string;
    fullName: string;
}
