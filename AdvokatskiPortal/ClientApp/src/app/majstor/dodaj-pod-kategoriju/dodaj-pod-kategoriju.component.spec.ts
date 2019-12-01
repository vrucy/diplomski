import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DodajPodKategorijuComponent } from './dodaj-pod-kategoriju.component';

describe('DodajPodKategorijuComponent', () => {
  let component: DodajPodKategorijuComponent;
  let fixture: ComponentFixture<DodajPodKategorijuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DodajPodKategorijuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DodajPodKategorijuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
