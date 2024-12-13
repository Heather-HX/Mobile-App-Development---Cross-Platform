import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.page.html',
  styleUrls: ['./countries.page.scss'],
})
export class CountriesPage implements OnInit {
  countries: any[] = [];

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.http
      .get<any[]>('https://restcountries.com/v2/all')
      .subscribe((data) => {
        this.countries = data;
      });
  }

  viewDetails(alpha3Code: string) {
    this.router.navigate(['/tabs/country-details'], {
      queryParams: { code: alpha3Code },
    });
  }
}
