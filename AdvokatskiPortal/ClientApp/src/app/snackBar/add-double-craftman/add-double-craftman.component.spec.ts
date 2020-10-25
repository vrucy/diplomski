import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDoubleCraftmanComponent } from './add-double-craftman.component';

describe('AddDoubleCraftmanComponent', () => {
  let component: AddDoubleCraftmanComponent;
  let fixture: ComponentFixture<AddDoubleCraftmanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddDoubleCraftmanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddDoubleCraftmanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
