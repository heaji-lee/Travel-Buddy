import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup  } from '@angular/forms';

import { DrawerModule } from 'primeng/drawer';
import { ButtonModule } from 'primeng/button';

import { AccordionModule } from 'primeng/accordion';
import { DatePickerModule } from 'primeng/datepicker';
import { MultiSelectModule } from 'primeng/multiselect';

@Component({
    selector: 'app-trip-form',
    standalone: true,
    imports: [
        CommonModule,
        DrawerModule,
        ButtonModule,
        AccordionModule,
        DatePickerModule,
        MultiSelectModule,
    ],
    templateUrl: './trip-form.component.html',
    styleUrl: './trip-form.component.css',
})
export class TripFormComponent {
    @Input() form!: FormGroup;
    @Input() title = '';
    @Input() subTitle = '';
    @Input() submitLabel = 'Save';

    @Input() companions: any[] = [];
    @Input() travelStyles: any[] = [];
    @Input() interests: any[] = [];

    @Output() submit = new EventEmitter<any>();

    onSubmit() {
        if (this.form.valid) {
          this.submit.emit(this.form.value);
        }
    }
}
