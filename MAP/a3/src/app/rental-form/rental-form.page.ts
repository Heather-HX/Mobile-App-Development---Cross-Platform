import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-rental-form',
  templateUrl: './rental-form.page.html',
  styleUrls: ['./rental-form.page.scss'],
})
export class RentalFormPage {
  reservation = {
    carType: '',
    date: '',
    hours: null,
    carSeat: false,
  };
  errorMessages: string[] = [];

  constructor(private router: Router) {}

  validateForm() {
    this.errorMessages = [];
    const today = new Date();
    const selectedDate = new Date(this.reservation.date);

    if (!this.reservation.carType) {
      this.errorMessages.push('Car Type: You must select a car type');
    }

    if (!this.reservation.date || selectedDate < today) {
      this.errorMessages.push(
        'Reservation Date: Date must be today or in the future'
      );
    }

    if (
      this.reservation.hours === null ||
      this.reservation.hours < 1 ||
      this.reservation.hours > 96
    ) {
      this.errorMessages.push('Hours: You must enter a value between 1-96');
    }

    return this.errorMessages.length === 0;
  }

  onReserve() {
    if (this.validateForm()) {
      // Navigate to the receipt page with reservation data
      this.router.navigate(['/receipt'], {
        state: { reservation: this.reservation },
      });
    }
  }

  onReset() {
    this.reservation = {
      carType: '',
      date: '',
      hours: null,
      carSeat: false,
    };
    this.errorMessages = [];
  }
}
