import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditSlucajComponent } from './edit-slucaj.component';

describe('EditSlucajComponent', () => {
  let component: EditSlucajComponent;
  let fixture: ComponentFixture<EditSlucajComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditSlucajComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditSlucajComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
