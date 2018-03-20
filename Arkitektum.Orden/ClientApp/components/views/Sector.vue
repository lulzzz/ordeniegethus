<template>
  <!--
  <div class="row">
    <div class="col-md-4">
      <form asp-action="Create">
        <div asp-validation-summary="ModelOnly" class="text-danger"></div>
        <input type="hidden" name="OrganizationId" value="@ViewData["CurrentOrganizationId"]"/>
        <div class="form-group">
          <label asp-for="Name" class="control-label"></label>
          <input asp-for="Name" class="form-control" />
          <span asp-validation-for="Name" class="text-danger"></span>
        </div>
        <div class="form-group">
          <input type="submit" value="Create" class="btn btn-default" />
        </div>
      </form>
    </div>
  </div>
-->
    <div>
      <form class="form">
      <div class="form-group">
        <label class="control-label">Navn</label>
        <input v-bind="name" class="form-control" />
      </div>
      <div class="form-group">
        <div v-if="context" v-for="lawReference in context.lawReferences">
            <resource-link v-bind:resource-link="lawReference"></resource-link>
        </div>
      </div>
        <div class="form-group">
          <input type="button" v-on:click="submit" class="btn btn-primary" value="Opprett"/>
        </div>
      </form>
        
    </div>
</template>

<script src="./Sector.js"></script>
<style src="./Sector.scss"></style>
