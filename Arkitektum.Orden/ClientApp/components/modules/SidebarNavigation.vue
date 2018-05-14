<template>
  <nav class="sidebar-navigation position-fixed" id="sidebar-navigation">
    <ul>
        <!--Navigation items-->
        <li v-for="navigationItem in navigationItems" v-bind:key="navigationItem.id">
          <a v-bind:href="getUrl(navigationItem)">{{ navigationItem.name }}</a>
        </li>
    </ul>
  </nav>
</template>

<script src="./SidebarNavigation.js"></script>
<style lang="scss" src="./SidebarNavigation.scss"></style>

