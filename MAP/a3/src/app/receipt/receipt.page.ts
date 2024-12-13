import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-receipt',
  templateUrl: './receipt.page.html',
  styleUrls: ['./receipt.page.scss'],
})
export class ReceiptPage implements OnInit {
  reservation: any;
  reservationId: string = '';
  subtotal = 0;
  tax = 0;
  total = 0;

  constructor(private router: Router) {
    const nav = this.router.getCurrentNavigation();
    this.reservation = nav?.extras?.state?.['reservation'] || {};
  }

  ngOnInit() {
    if (this.reservation) {
      this.calculateCost();
      this.reservationId = Math.floor(1000 + Math.random() * 9000).toString();
    }
  }

  calculateCost() {
    const hourlyRate = this.reservation.carType === 'sedan' ? 7 : 12;
    const dailyRate = this.reservation.carType === 'sedan' ? 70 : 100;
    const carSeatRate = this.reservation.carSeat ? (this.reservation.hours > 5 ? 10 : 1) : 0;

    if (this.reservation.hours > 5) {
      const days = Math.ceil(this.reservation.hours / 24);
      this.subtotal = dailyRate * days + carSeatRate * days;
    } else {
      this.subtotal = hourlyRate * this.reservation.hours + carSeatRate * this.reservation.hours;
    }

    this.tax = this.subtotal * 0.13;
    this.total = this.subtotal + this.tax;
  }
}
