import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SlanjeSlucajaComponent } from './slanje-slucaja.component';

describe('SlanjeSlucajaComponent', () => {
  let component: SlanjeSlucajaComponent;
  let fixture: ComponentFixture<SlanjeSlucajaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SlanjeSlucajaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SlanjeSlucajaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
