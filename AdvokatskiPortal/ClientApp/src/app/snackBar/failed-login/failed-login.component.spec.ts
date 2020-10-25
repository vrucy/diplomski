import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FaliedLoginComponent } from './failed-login.component';

describe('FaliedLoginComponent', () => {
  let component: FaliedLoginComponent;
  let fixture: ComponentFixture<FaliedLoginComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FaliedLoginComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FaliedLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
