import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RentalFormPage } from './rental-form.page';

describe('RentalFormPage', () => {
  let component: RentalFormPage;
  let fixture: ComponentFixture<RentalFormPage>;

  beforeEach(() => {
    fixture = TestBed.createComponent(RentalFormPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
