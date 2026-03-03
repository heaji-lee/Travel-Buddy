import { Component } from '@angular/core';
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
    isLoginDialogVisible = false;

    showLoginDialog(){
        this.isLoginDialogVisible = true;
    }
}
