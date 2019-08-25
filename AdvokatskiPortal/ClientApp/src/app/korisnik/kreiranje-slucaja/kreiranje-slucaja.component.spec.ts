import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KreiranjeSlucajaComponent } from './kreiranje-slucaja.component';

describe('KreiranjeSlucajaComponent', () => {
  let component: KreiranjeSlucajaComponent;
  let fixture: ComponentFixture<KreiranjeSlucajaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KreiranjeSlucajaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KreiranjeSlucajaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
