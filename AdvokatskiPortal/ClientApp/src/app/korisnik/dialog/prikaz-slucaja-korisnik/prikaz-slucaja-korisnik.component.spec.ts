import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrikazSLucajaKorisnikComponent } from './prikaz-slucaja-korisnik.component';

describe('PrikazSLucajaKorisnikComponent', () => {
  let component: PrikazSLucajaKorisnikComponent;
  let fixture: ComponentFixture<PrikazSLucajaKorisnikComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrikazSLucajaKorisnikComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrikazSLucajaKorisnikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
