import { Component } from '@angular/core';
import { RouterLink } from "@angular/router";
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, ButtonModule, DialogModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  isInfoDialogVisible = false;

  showDialog() {
    this.isInfoDialogVisible = true;
  }
}
