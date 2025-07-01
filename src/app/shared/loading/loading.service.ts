import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
    providedIn: "root",
})
export class LoadingService {
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private count: number = 0;
    loading$ = this.loadingSubject.asObservable();

    loadingOn() {
        this.count++;
        this.loadingSubject.next(true);
    }

    loadingOff() {
        this.count--;

        if (this.count === 0) {
            this.loadingSubject.next(false);
        }

    }
}