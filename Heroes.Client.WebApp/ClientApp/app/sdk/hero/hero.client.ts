import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";

@Injectable()
export class HeroClient {

	get(key: string): Observable<string> {
		console.log("HeroClient :: get", key);
		return  Observable.of("hero retrieved");
	}

}