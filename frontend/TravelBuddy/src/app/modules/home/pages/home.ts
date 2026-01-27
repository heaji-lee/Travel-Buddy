import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Button } from 'primeng/button';

@Component({
    selector: 'app-home',
    imports: [Button, RouterLink],
    templateUrl: './home.html',
    styleUrl: './home.css',
})
export class Home {}
