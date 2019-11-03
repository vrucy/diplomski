import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditMajstorComponent } from './edit-majstor.component';

describe('EditMajstorComponent', () => {
  let component: EditMajstorComponent;
  let fixture: ComponentFixture<EditMajstorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditMajstorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditMajstorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
