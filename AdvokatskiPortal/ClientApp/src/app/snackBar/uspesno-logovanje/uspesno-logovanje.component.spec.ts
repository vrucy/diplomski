import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UspesnoLogovanjeComponent } from './uspesno-logovanje.component';

describe('UspesnoLogovanjeComponent', () => {
  let component: UspesnoLogovanjeComponent;
  let fixture: ComponentFixture<UspesnoLogovanjeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UspesnoLogovanjeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UspesnoLogovanjeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
