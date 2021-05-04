import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FetchWeatherdataComponent } from './fetch-weatherdata.component';

describe('FetchWeatherdataComponent', () => {
  let component: FetchWeatherdataComponent;
  let fixture: ComponentFixture<FetchWeatherdataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FetchWeatherdataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FetchWeatherdataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
