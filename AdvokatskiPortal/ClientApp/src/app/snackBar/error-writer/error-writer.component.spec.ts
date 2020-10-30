import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ErrorWriterComponent } from './error-writer.component';

describe('ErrorWriterComponent', () => {
  let component: ErrorWriterComponent;
  let fixture: ComponentFixture<ErrorWriterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ErrorWriterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ErrorWriterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
