import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SendCaseComponent } from './send-case.component';

describe('SendCaseComponent', () => {
  let component: SendCaseComponent;
  let fixture: ComponentFixture<SendCaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SendCaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SendCaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
