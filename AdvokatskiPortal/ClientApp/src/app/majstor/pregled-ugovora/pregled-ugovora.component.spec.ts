import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PregledUgovoraComponent } from './pregled-ugovora.component';

describe('PregledUgovoraComponent', () => {
  let component: PregledUgovoraComponent;
  let fixture: ComponentFixture<PregledUgovoraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PregledUgovoraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PregledUgovoraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
