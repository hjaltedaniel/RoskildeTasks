<template>
  <div v-if="messages.length != 0" class="d-flex flex-column message-view">
    <div
      class="single-message d-flex align-items-center"
      v-bind:class="{ adminMessage: message.isFromAdmin }"
      v-for="message in olderMessages"
      :key="message.Date"
    >
      <span class="single-message__time">{{getTimeFromDate(message.Date)}}</span>
      <div class="single-message__content">
        <span class="content">{{message.Content}}</span>
      </div>
    </div>
    <div
      class="message-view__breaker d-flex justify-content-center"
      v-if="yesterdaysMessages != ''"
    >
      <span class="breaker-line"></span>
      <span class="breaker-content">Yesterday</span>
    </div>
    <div
      class="single-message d-flex align-items-center"
      v-bind:class="{ adminMessage: message.isFromAdmin }"
      v-for="message in yesterdaysMessages"
      :key="message.Date"
    >
      <span class="single-message__time">{{getTimeFromDate(message.Date)}}</span>
      <div class="single-message__content">
        <span class="content">{{message.Content}}</span>
      </div>
    </div>
    <div class="message-view__breaker d-flex justify-content-center" v-if="todaysMessages != ''">
      <span class="breaker-line"></span>
      <span class="breaker-content">Today</span>
    </div>
    <div
      class="single-message d-flex align-items-center"
      v-bind:class="{ adminMessage: message.isFromAdmin }"
      v-for="message in todaysMessages"
      :key="message.Date"
    >
      <span class="single-message__time">{{getTimeFromDate(message.Date)}}</span>
      <div class="single-message__content">
        <span class="content">{{message.Content}}</span>
      </div>
    </div>
  </div>
  <div v-else class="d-flex flex-column message-view">
    <div class="single-message d-flex align-items-center adminMessage">
      <div class="single-message__content">
        <span class="content font-italic">{{noMessage}}</span>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  components: {},
  props: {
    messages: { required: true },
    noMessage: { required: true }
  },
  data() {
    return {
      todaysMessages: [],
      yesterdaysMessages: [],
      olderMessages: []
    };
  },
  computed: {},
  mounted() {
    this.groupMessages();
  },
  watch: {
    messages: function() {
      this.groupMessages();
    }
  },
  methods: {
    getTimeFromDate(date) {
      const d = new Date(date);
      const min = d.getMinutes();
      const hr = d.getHours();
      let minutes;
      if (min <= 9) {
        minutes = "0" + d.getMinutes();
      } else {
        minutes = d.getMinutes();
      }

      let hours;
      if (hr <= 9) {
        hours = "0" + d.getHours();
      } else {
        hours = d.getHours();
      }

      return hours + ":" + minutes;
    },
    groupMessages() {
      let today = new Date();
      today.setHours(0, 0, 0, 0);
      let todaysMessages = [];
      let yesterdaysMessages = [];
      let olderMessages = [];

      this.messages.forEach(function(message, index) {
        const d = new Date(message.Date);
        d.setHours(0, 0, 0, 0);

        const diffTime = Math.abs(today - d);
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

        if (returnDate(d) == returnDate(today)) {
          todaysMessages.push(message);
        } else if (diffDays == 1 && returnDate(d) != returnDate(today)) {
          yesterdaysMessages.push(message);
        } else {
          olderMessages.push(message);
        }

        function returnDate(date) {
          var dd = String(date.getDate()).padStart(2, "0");
          var mm = String(date.getMonth() + 1).padStart(2, "0"); //January is 0!
          var yyyy = date.getFullYear();
          return dd + "/" + mm + "/" + yyyy;
        }
      });
      this.olderMessages = olderMessages;
      this.yesterdaysMessages = yesterdaysMessages;
      this.todaysMessages = todaysMessages;
    }
  }
};
</script>

<style lang="scss" scoped>
@import "MessageView";
</style>
