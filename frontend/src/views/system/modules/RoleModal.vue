<template>
  <a-modal :title="title" :width="800" :visible="visible" :confirmLoading="confirmLoading" @ok="handleOk"
    @cancel="handleCancel">
    <a-spin :spinning="spinningLoading">
      <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules">
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="角色名称" prop="roleName" hasFeedback>
              <a-input placeholder="请输入角色名称" v-model="form.roleName" />
            </a-form-model-item>
          </a-col>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="角色描述" prop="describe" hasFeedback>
              <a-input placeholder="请输入角色名称" v-model="form.describe" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="24">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="是否启用">
              <a-switch  checkedChildren="是" v-model="form.enable" unCheckedChildren="否" />
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
// import { getPermissions } from '@/api/manage'
import pick from 'lodash.pick'
function createForm() {
  return {
    roleName: '',
    describe: '',
    enable: true,
  }
}
export default {
  name: 'RoleModal',
  data() {
    return {
      labelCol: {
        md: { span: 6 },
        sm: { span: 4 }
      },
      wrapperCol: {
        md: { span: 14 },
        sm: { span: 20 }
      },
      gutter: 24,
      mdSize: 24,
      smSize: 24,
      formType: 'add',
      title: '添加角色',
      spinningLoading: false,
      visible: false,
      confirmLoading: false,
      rules: {
        roleName: [{ required: true, min: 1, message: '请输入角色名称！' }],
        describe: [{ required: true, min: 1, message: '请输入角色描述！' }]
      },
      form: {}
    }
  },
  methods: {
    // 打开页面初始化
    detail(value) {
      this.visible = true
      if (value) {
        this.form = JSON.parse(JSON.stringify(value))
        // 删除多余属性
        delete this.form.permissions;
        this.formType = 'edit'
        this.title = '编辑角色'
      }
      else {
        this.title = '新建角色'
        this.formType = 'add'
        this.form = createForm()
      }
    },
    add() {
      this.edit({ id: 0 })
    },
    edit(record) {
      this.mdl = Object.assign({}, record)
      this.visible = true

      // 有权限表，处理勾选
      if (this.mdl.permissions && this.permissions) {
        // 先处理要勾选的权限结构
        const permissionsAction = {}
        this.mdl.permissions.forEach(permission => {
          permissionsAction[permission.permissionId] = permission.actionEntitySet.map(entity => entity.action)
        })
        // 把权限表遍历一遍，设定要勾选的权限 action
        this.permissions.forEach(permission => {
          permission.selected = permissionsAction[permission.id] || []
        })
      }

      this.$nextTick(() => {
        this.form.setFieldsValue(pick(this.mdl, 'id', 'name', 'status', 'describe'))
      })
      console.log('this.mdl', this.mdl)
    },
    close() {
      this.$emit('close')
      this.visible = false
    },
    handleOk() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.confirmLoading = true
          if (this.formType === 'edit') {
            this.$http.put('/sysRole', this.form).then(res => {
              if (res.code === 200) {
                this.$emit('ok')
                this.confirmLoading = false
                this.visible = false
              } else {
                this.$message.warning(res.message)
              }
            })
          }
          else {
            this.$http.post('/sysRole', this.form).then(res => {
              if (res.code === 200) {
                this.$emit('ok')
                this.confirmLoading = false
                this.visible = false
              } else {
                this.$message.warning(res.message)
              }
            })
          }
        }
      })
    },
    handleCancel() {
      this.close()
    },
  }
}
</script>

<style scoped></style>
