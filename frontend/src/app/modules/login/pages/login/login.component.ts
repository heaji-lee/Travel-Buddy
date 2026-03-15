import { Component, inject} from '@angular/core';
import { FormBuilder } from '@angular/forms';

import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from "primeng/dialog";

@Component({
  selector: 'app-login.component',
  imports: [ButtonModule, DividerModule, InputTextModule, DialogModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
    private readonly fb = inject(FormBuilder);
    isLoginDialogVisible = false;

    showLoginDialog(){
        this.isLoginDialogVisible = true;
    }

    login() {
        console.log('Loggin in')
    }

    signUp() {
        console.log('Signing up')
    }
}
