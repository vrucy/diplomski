import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PregledSlucajaKorisnikComponent } from './pregled-slucaja-korisnik.component';

describe('PregledSlucajaKorisnikComponent', () => {
  let component: PregledSlucajaKorisnikComponent;
  let fixture: ComponentFixture<PregledSlucajaKorisnikComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PregledSlucajaKorisnikComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PregledSlucajaKorisnikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
