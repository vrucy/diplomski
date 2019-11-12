import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ObavestenjeKreirajSlucajComponent } from './obavestenje-kreiraj-slucaj.component';

describe('ObavestenjeKreirajSlucajComponent', () => {
  let component: ObavestenjeKreirajSlucajComponent;
  let fixture: ComponentFixture<ObavestenjeKreirajSlucajComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ObavestenjeKreirajSlucajComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ObavestenjeKreirajSlucajComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
