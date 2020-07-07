import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UspesnoStePoslaliSlucajComponent } from './uspesno-ste-poslali-slucaj.component';

describe('UspesnoStePoslaliSlucajComponent', () => {
  let component: UspesnoStePoslaliSlucajComponent;
  let fixture: ComponentFixture<UspesnoStePoslaliSlucajComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UspesnoStePoslaliSlucajComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UspesnoStePoslaliSlucajComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
