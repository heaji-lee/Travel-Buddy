import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Drawer, DrawerModule } from 'primeng/drawer';

@Component({
    selector: 'app-trip-drawer',
    imports: [CommonModule, DrawerModule],
    templateUrl: './trip-drawer.component.html',
    styleUrl: './trip-drawer.component.css',
})
export class TripDrawerComponent {
    @Input() visible = false;
    @Input() trip: any = null;
    @Output() visibleChange = new EventEmitter<boolean>();

    close() {
        this.visibleChange.emit(false);
    }
}
