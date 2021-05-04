import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-fetch-weatherdata',
  templateUrl: './fetch-weatherdata.component.html',
  styleUrls: ['./fetch-weatherdata.component.scss']
})
export class FetchWeatherdataComponent implements OnInit {
  public weatherdata: WeatherForecast[];

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.get('/weatherdata/weatherforecast').subscribe(result => {
      this.weatherdata = result as WeatherForecast[];
    }, (error) => {
      console.error(error);
    })
  }

}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
