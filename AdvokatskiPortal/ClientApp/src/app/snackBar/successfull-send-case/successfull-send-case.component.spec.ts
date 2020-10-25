import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SuccessfullSendCaseComponent } from './successfull-send-case.component';

describe('SuccessfullSendCaseComponent', () => {
  let component: SuccessfullSendCaseComponent;
  let fixture: ComponentFixture<SuccessfullSendCaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SuccessfullSendCaseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SuccessfullSendCaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
