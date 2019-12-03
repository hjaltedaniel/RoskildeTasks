<template>
	<div v-if="messages.length != 0" class="d-flex flex-column message-view">
		<div class="single-message d-flex align-items-center" v-bind:class="{ adminMessage: message.isFromAdmin }" v-for="message in messages" :key="message.Date">
			<span class="single-message__time">
				{{getTimeFromDate(message.Date)}}
			</span>
			<div class="single-message__content">
				<span class="content">
					{{message.Content}}
				</span>
			</div>
		</div>
	</div>
	<div v-else class="d-flex flex-column message-view">
		<div class="single-message d-flex align-items-center adminMessage">
			<div class="single-message__content">
				<span class="content font-italic">
					{{noMessage}}
				</span>
			</div>
		</div>
	</div>
</template>

<script>
	export default {
		components: {},
		props: {
			messages: { required: true },
			noMessage: {required: true},
		},
		data () {
			return {}
		},
		computed: {
		},
		mounted() {
		},
		watch: {
		},
		methods: {
			getTimeFromDate(date) {
				const d = new Date(date);
				const min = d.getMinutes();
				const hr = d.getHours();
				let minutes;
				if (min <= 9) {
					minutes = "0" + d.getMinutes();
				}
				else {
					minutes = d.getMinutes();
				}

				let hours;
				if (hr <= 9) {
					hours = "0" + d.getHours();
				}
				else {
					hours = d.getHours();
				}

				return hours + ":" + minutes;
			}
		}
}



</script>

<style lang="scss" scoped>
@import "MessageView";
</style>
