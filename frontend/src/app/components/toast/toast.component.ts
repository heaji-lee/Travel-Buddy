import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { Tag } from 'primeng/tag'

@Component({
    selector: 'app-toast',
    imports: [CommonModule, ButtonModule, ToastModule, Tag],
    templateUrl: './toast.component.html',
    styleUrl: './toast.component.css',
})
export class ToastComponent {}