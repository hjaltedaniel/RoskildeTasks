import store from './../../store';

export const protectedRouteGuard = (to, from, next) => {
	if (store.state.user) {
		next();
	}
	else {
		next("/login")
	}
}