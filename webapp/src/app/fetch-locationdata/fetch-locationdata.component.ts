import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-fetch-locationdata',
  templateUrl: './fetch-locationdata.component.html',
  styleUrls: ['./fetch-locationdata.component.scss']
})
export class FetchLocationdataComponent implements OnInit {
  public locationdata: LocationInformation[];

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.get('/locationdata/locations').subscribe(result => {
      this.locationdata = result as LocationInformation[];
    }, (error) => {
      console.error(error);
    })
  }

}

interface LocationInformation {
  name: string;
  country: string;
  countryCode: string;
  population: number;
}
