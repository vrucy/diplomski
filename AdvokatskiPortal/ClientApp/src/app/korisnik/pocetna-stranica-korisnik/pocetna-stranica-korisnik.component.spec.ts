import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PocetnaStranicaKorisnikComponent } from './pocetna-stranica-korisnik.component';

describe('PocetnaStranicaKorisnikComponent', () => {
  let component: PocetnaStranicaKorisnikComponent;
  let fixture: ComponentFixture<PocetnaStranicaKorisnikComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PocetnaStranicaKorisnikComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PocetnaStranicaKorisnikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
