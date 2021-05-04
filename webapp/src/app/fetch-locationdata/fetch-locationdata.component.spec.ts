import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FetchLocationdataComponent } from './fetch-locationdata.component';

describe('FetchLocationdataComponent', () => {
  let component: FetchLocationdataComponent;
  let fixture: ComponentFixture<FetchLocationdataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FetchLocationdataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FetchLocationdataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
