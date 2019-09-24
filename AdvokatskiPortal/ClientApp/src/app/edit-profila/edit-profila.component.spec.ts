import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProfilaComponent } from './edit-profila.component';

describe('EditProfilaComponent', () => {
  let component: EditProfilaComponent;
  let fixture: ComponentFixture<EditProfilaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditProfilaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditProfilaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
