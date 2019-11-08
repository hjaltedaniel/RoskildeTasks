<template>
  <aside class="main-menu column is-1">
    <router-link to="/">
      <div class="header-logo">
        <img src="../assets/Icons/canopy.svg" alt="RF Tasks" class="header-logo__image" />
      </div>
    </router-link>
    <ul class="main-menu-list">
      <router-link to="/">
        <li class="main-menu-list__item is-active">
          <img src="../assets/Icons/tasks.svg" alt="Tasks" class="item-icon" />
          <span class="item-description">Tasks</span>
        </li>
      </router-link>
      <router-link to="/messages">
        <li class="main-menu-list__item">
          <img src="../assets/Icons/comments.svg" alt="Messages" class="item-icon" />
          <span class="item-description">Messages</span>
        </li>
      </router-link>
      <router-link to="/ressources">
        <li class="main-menu-list__item">
          <img src="../assets/Icons/copy.svg" alt="Ressources" class="item-icon" />
          <span class="item-description">Ressources</span>
        </li>
      </router-link>
      <router-link to="/profile">
        <li class="main-menu-list__item">
          <img src="../assets/Icons/user.svg" alt="Profile" class="item-icon" />
          <span class="item-description">Profile</span>
        </li>
      </router-link>
    </ul>
  </aside>
</template>

<script>
export default {};
</script>

<style>
</style>