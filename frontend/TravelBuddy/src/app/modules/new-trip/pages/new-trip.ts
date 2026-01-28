import { Component } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { InputTextModule } from 'primeng/inputtext';
import { AccordionModule } from 'primeng/accordion';
import { Button } from "primeng/button";

@Component({
  selector: 'app-new-trip',
  imports: [FormsModule, InputTextModule, AccordionModule, Button],
  templateUrl: './new-trip.html',
  styleUrl: './new-trip.css',
})
export class NewTrip {

}
